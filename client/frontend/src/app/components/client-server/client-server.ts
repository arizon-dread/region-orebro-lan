import { Component, effect, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SettingsService } from '../../shared/services/settings.service';
import { HttpService } from '../../shared/services/http.service';
import { take } from 'rxjs';
import { Setting } from '../../shared/models/setting';

@Component({
  selector: 'app-client-server',
  imports: [ReactiveFormsModule],
  templateUrl: './client-server.html',
  styleUrl: './client-server.css',
})
export class ClientServer implements OnInit {

  form = new FormGroup({
    serverUrl: new FormControl<string>('', Validators.required),
    isServer: new FormControl<boolean>(false),
  });
  serverUrl: Setting | undefined;
  isServer: boolean | undefined;

  constructor(private settingsSvc: SettingsService, private http: HttpService) {
    effect(() => {
        this.form.controls.isServer.setValue(this.settingsSvc.isServerSig());
        this.isServer = settingsSvc.isServerSig();
    })
  }
  async ngOnInit() {
    console.log("isServer: " + this.isServer);
    this.form.controls.isServer.setValue(this.settingsSvc.isServerSig());
    this.http.getSettings().pipe(take(1)).subscribe({
      next: (data: Setting[]) => {
        if (data) {
          data.map((item) => {
            if (item.key === "ServerAddress") {
              this.form.controls.serverUrl.setValue(item.value);
              this.serverUrl = item;
            }
          })
        }
      }
    })
  }
  toggleServerClient() {
    if (this.form.controls.isServer.value) {
      this.form.controls.serverUrl.setValue("");
      this.saveServerUrl();
    }
    console.log("isServer: " + this.form.controls.isServer.value);
    this.settingsSvc.setIsServer(this.form.controls.isServer.value ?? false);
  }
  saveServerUrl() {
    if (this.serverUrl) {
      this.serverUrl.value = this.form.controls.serverUrl.value ?? "";
      this.http.saveSetting(this.serverUrl).pipe(take(1)).subscribe({
        next: (data: Setting) => {
          if (data) {
            this.serverUrl = data;
            this.form.controls.serverUrl.setValue(data.value);
          }
        }
      })
    }

  }
}
