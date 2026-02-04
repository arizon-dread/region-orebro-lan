import { Injectable, signal, WritableSignal } from '@angular/core';
import { HttpService } from './http.service';
import { LocalStorageService } from './local-storage.service';
import { of, take } from 'rxjs';
import { Setting } from '../models/setting';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  constructor(private http: HttpService, private localStorageSvc: LocalStorageService) { }
  private isServerSetting: Setting | undefined;
  public isServerSig: WritableSignal<boolean> = signal<boolean>(false);
  public isServer(): Promise<boolean> {
    if (this.isServerSetting) {
      return new Promise<boolean>((resolve, reject) => { resolve(this.isServerSetting?.value === "Server") });
    }
    return new Promise<boolean>((resolve, reject) => {
      this.http.getSettings().subscribe({
        next: (data: Setting[]) => {
          if (data) {
            data.map((item) => {
              if (item.key === 'ServiceMode') {
                this.isServerSetting = item;
                if (item.value === 'Server') {
                  resolve(true);
                } else {
                  resolve(false);
                }
              }
            });
          }
        },
        error: (err: HttpErrorResponse) => {
          resolve(false);
        }
      })
    }).then((value) => {
      return value;
    });
  }
  public setIsServer(isServer: boolean) {
    if (this.isServerSetting) {
      this.isServerSetting!.value = isServer ? "Server" : "Client";
      this.isServerSig.set(isServer);

      this.http.saveSetting(this.isServerSetting).pipe(take(1)).subscribe({
        next: (data: Setting) => {
          if (data) {
            this.isServerSetting = data;
          }
        }
      });
    } else {
      this.isServer().then((value) => {
        this.setIsServer(value);
      });
    }
  }
}
