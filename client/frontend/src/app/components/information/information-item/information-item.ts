import { Component, input } from '@angular/core';
import { Info } from '../../../shared/models/info';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-information-item',
  imports: [DatePipe],
  templateUrl: './information-item.html',
  styleUrl: './information-item.css',
})
export class InformationItem {
  readonly infoItem = input<Info | undefined>();
}
