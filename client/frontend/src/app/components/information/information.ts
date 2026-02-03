import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { Subscription, take } from 'rxjs';
import { Info } from '../../shared/models/info';
import { HttpErrorResponse } from '@angular/common/http';
import { InformationItem } from "./information-item/information-item";
import { AddInformation } from "./add-information/add-information";
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-information',
  imports: [InformationItem],
  templateUrl: './information.html',
  styleUrl: './information.css',
})
export class Information implements OnInit {

  informationList: WritableSignal<Info[] | undefined> = signal(undefined);
  infoDialogRef: MatDialogRef<AddInformation> | undefined;
  backdropClickSub: Subscription | undefined;
  afterClosedSub: Subscription | undefined;
  constructor(private httpSvc: HttpService, private infoDialog: MatDialog) {
  }
  ngOnInit(): void {
    this.getInfo();
  }
  getInfo() {
    this.httpSvc.getInformationList().pipe(take(1)).subscribe({
      next: (data: Info[]) => {
        if (data) {
          this.informationList.set(data);
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error(JSON.stringify(err));
      }
    });
  }
  unpublish(infoItem: Info) {
    infoItem.unpublished = new Date();
    this.update(infoItem);

  }
  update(infoItem: Info) {
    this.httpSvc.saveInformation(infoItem).pipe(take(1)).subscribe({
      next: (data: Info) => {
        this.getInfo();
        //fine and dandy, toastr
      },
      error: (err: HttpErrorResponse) => {
        console.error(JSON.stringify(err));
        //här borde vi göra nåt att vi fick conflict från servern för att den redan var ändrad. 
      }
    });
  }
  async edit(infoItem: Info) {
    await this.popupModal(infoItem);
  }
  async popupModal(infoItem?: Info) {
    await new Promise<Info>((resolve, reject) => {
      this.infoDialogRef = this.infoDialog.open(AddInformation, {
        height: 'fit-content',
        width: 'fit-content',
        backdropClass: 'cdk-overlay-dark-backdrop',
        hasBackdrop: true,
        data: infoItem,
        panelClass: 'custom-popup'
      });
      this.backdropClickSub = this.infoDialogRef.backdropClick().subscribe(() => {
        if (this.infoDialogRef != undefined) {
          this.infoDialogRef.close();
        }
        reject();
      });
      this.infoDialogRef.afterClosed().subscribe({
        next: (data: Info) => {
          resolve(data);
        }
      });
    }
    ).then((data) => {
      if (data) {
        this.update(data);
      }
      if (this.afterClosedSub) {
        this.afterClosedSub.unsubscribe();
      }
      if (this.backdropClickSub) {
        this.backdropClickSub.unsubscribe();
      }
    });
  }
}
