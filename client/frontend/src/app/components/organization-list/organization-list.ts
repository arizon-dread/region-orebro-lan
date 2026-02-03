import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { take } from 'rxjs';
import { Info } from '../../shared/models/info';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-organization-list',
  imports: [],
  templateUrl: './organization-list.html',
  styleUrl: './organization-list.css',
})
export class OrganizationList implements OnInit {

  informationList: WritableSignal<Info[] | undefined> = signal(undefined);
  constructor(private httpSvc: HttpService) {
  }
  ngOnInit(): void {
    this.httpSvc.getInformationList().pipe(take(1)).subscribe({
      next: (data: Info[]) => {
        if (data) {
          this.informationList.set(data);
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error(err.message);
      }
    })
  }

}
