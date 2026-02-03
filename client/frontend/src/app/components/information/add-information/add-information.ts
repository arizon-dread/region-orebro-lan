import { Component, Inject, Injectable } from '@angular/core';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpService } from '../../../shared/services/http.service';
import { Info } from '../../../shared/models/info';
import { take } from 'rxjs';
import { MatDialogContent, MatDialogModule, MatDialogActions, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpErrorResponse } from '@angular/common/http';
@Injectable({providedIn: 'root'})
@Component({
  selector: 'app-add-information',
  imports: [ReactiveFormsModule, MatDialogContent, MatDialogModule, MatDialogActions, ],
  templateUrl: './add-information.html',
  styleUrl: './add-information.css',
})
export class AddInformation {

  form: FormGroup = new FormGroup({
    title: new FormControl<string>("", Validators.required),
    text: new FormControl<string>("", Validators.required),
  });

  constructor(private httpSvc: HttpService, @Inject(MAT_DIALOG_DATA) private data: {title: string, text: string}) { }
  reset() {
    this.form.reset();
    this.form.markAsUntouched();
    this.form.markAsPristine();
  }
  submit() : Info | undefined {
    //validate form
    if (this.form.valid) {
      let info: Info = {
        id: crypto.randomUUID(),
        title: this.form.controls.title.value,
        text: this.form.controls.text.value,
        publishDate: new Date(),
      }
      return info;
      
    }
    return undefined;
  }
}
