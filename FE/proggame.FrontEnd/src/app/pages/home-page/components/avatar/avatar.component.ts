import { Component, ElementRef, ViewChild } from '@angular/core';

interface WindowWithFilePicker extends Window {
  showSaveFilePicker: (options?: any) => Promise<any>;
}

declare const window: WindowWithFilePicker;
@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss'],
})
export class AvatarComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  uploadedFile: File | null = null;
  uploadIMG: string =
    'https://xsgames.co/randomusers/assets/avatars/pixel/32.jpg';

  filesToSave: File[] = [];

  constructor() {}

  //#region methods

  onDragOver(event: any) {
    event.preventDefault();
    event.stopPropagation();
    console.log('dragover');
  }

  onDragLeave(event: any) {
    event.preventDefault();
    event.stopPropagation();
    console.log('dragleave');
  }

  onFileDropped(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      this.uploadedFile = files[0];
    }
    console.log('dragdrop');
  }

  onFileSelected(event: any) {
    const files = event.target.files;
    if (files && files.length > 0) {
      this.uploadedFile = files[0];
    }
  }

  saveFile() {
    if (this.uploadedFile) {
      this.filesToSave.push(this.uploadedFile);
      this.uploadedFile = null; // Clear the uploadedFile after saving
    }
  }

  async downloadFile(file: File) {
    if ('showSaveFilePicker' in window) {
      const fileHandle = await window.showSaveFilePicker();
      const writable = await fileHandle.createWritable();
      await writable.write(file);
      await writable.close();
    }
  }

  //#endregion
}
