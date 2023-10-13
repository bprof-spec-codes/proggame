import { Component, ElementRef, ViewChild } from '@angular/core';

interface WindowWithFilePicker extends Window {
  showSaveFilePicker: (options?: any) => Promise<any>;
}

declare const window: WindowWithFilePicker;
@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss'],
})
export class UploadComponent {
  constructor() {}
}
