import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Info } from '../models/info';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private apiPath: string = "http://localhost:5111";
  private http = inject(HttpClient);

  get(){
    this.http.get<string>(this.apiPath + '/ready').subscribe((res) => {
       console.log('Response: ', res);
    });
  }
  getInformationList() : Observable<Info[]> {
    return this.http.get<Info[]>(this.apiPath + '/api/v1/info');
  }
}
