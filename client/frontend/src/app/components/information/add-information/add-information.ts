import { Component, Inject, Injectable, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpService } from '../../../shared/services/http.service';
import { Info } from '../../../shared/models/info';
import { take } from 'rxjs';
import { MatDialogContent, MatDialogModule, MatDialogActions, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpErrorResponse } from '@angular/common/http';
@Injectable({ providedIn: 'root' })
@Component({
  selector: 'app-add-information',
  imports: [ReactiveFormsModule, MatDialogContent, MatDialogModule, MatDialogActions,],
  templateUrl: './add-information.html',
  styleUrl: './add-information.css',
})
export class AddInformation implements OnInit {

  form: FormGroup = new FormGroup({
    title: new FormControl<string>("", Validators.required),
    text: new FormControl<string>("", Validators.required),
  });


  constructor(private httpSvc: HttpService, @Inject(MAT_DIALOG_DATA) private data: Info | undefined) { }
  ngOnInit() {
    if (this.data) {
      this.form.patchValue({
        title: this.data.title,
        text: this.data.text,
      });
      this.form.updateValueAndValidity();
    }
  }
  reset() {
    this.form.reset();
    this.form.markAsUntouched();
    this.form.markAsPristine();
  }
  submit(): Info | undefined {
    //validate form
    let id: string;
    let version: number;
    if (!this.data?.id) {
      id = crypto.randomUUID();
      version = 1;
    } else {
      id = this.data.id;
      version = this.data.version;
    }
    if (this.form.valid) {
      let info: Info = {
        id: id,
        title: this.form.controls.title.value,
        text: this.form.controls.text.value,
        version: version,
      }
      return info;

    }
    return undefined;
  }
}
