import { Component, effect, EventEmitter, input, output } from '@angular/core';
import { Info } from '../../../shared/models/info';
import { DatePipe } from '@angular/common';
import { SettingsService } from '../../../shared/services/settings.service';

@Component({
  selector: 'app-information-item',
  imports: [DatePipe],
  templateUrl: './information-item.html',
  styleUrl: './information-item.css',
})
export class InformationItem {
  readonly infoItem = input<Info | undefined>();
  unpublishItem = output<Info>();
  editItem = output<Info>();
  displayButtons: boolean = false;

  constructor(private settingSvc: SettingsService) {
    effect(() => {
      this.displayButtons = this.settingSvc.isServerSig();
    });
  }
  unpublish() {
    this.unpublishItem.emit(this.infoItem()!);
  }
  edit() {
    this.editItem.emit(this.infoItem()!);
  }
}
