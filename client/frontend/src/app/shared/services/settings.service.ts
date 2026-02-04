import { Injectable, OnInit, signal, WritableSignal } from '@angular/core';
import { HttpService } from './http.service';
import { LocalStorageService } from './local-storage.service';
import { of, take } from 'rxjs';
import { Setting } from '../models/setting';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  constructor(private http: HttpService, private localStorageSvc: LocalStorageService) { 
 this.http.getSettings().subscribe({
        next: (data: Setting[]) => {
          if (data) {
            data.map((item) => {
              if (item.key === 'ServiceMode') {
                console.log("serverSetting servicemode, value: " + item.value)
                this.isServerSetting = item;
                if (item.value === 'Server') {
                  this.isServerSig.set(true);
                } else {
                  this.isServerSig.set(false);
                }
              }
            });
          }
        },
        error: (err: HttpErrorResponse) => {
        }
      });
  }
  private isServerSetting: Setting | undefined;
  public isServerSig: WritableSignal<boolean> = signal<boolean>(false);

  public setIsServer(isServer: boolean) {
    this.isServerSetting!.value = isServer ? "Server" : "Client";
    this.isServerSig.set(isServer);
    console.log("isServer: " + isServer);
    this.http.saveSetting(this.isServerSetting!).pipe(take(1)).subscribe({
      next: (data: Setting) => {
        if (data) {
          this.isServerSetting = data;
        }
      }
    });

  }
}
