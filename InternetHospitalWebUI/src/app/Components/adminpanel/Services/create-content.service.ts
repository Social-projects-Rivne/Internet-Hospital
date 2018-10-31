import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Content } from '../../../Models/Content';

@Injectable({
  providedIn: 'root'
})
// Now this service is the same as EditContent.
export class CreateContentService {

  constructor() { }

  form: FormGroup = new FormGroup({
    Id: new FormControl({value: '', disabled: true}),
    Title: new FormControl('', Validators.required),
    Source: new FormControl('', Validators.required),
    Images: new FormControl('', Validators.required),
    Body: new FormControl('', Validators.required)
  });
  files: string[];

  initializeFormGroup() {
    this.form.setValue({
      Id: '',
      Title: '',
      Body: '',
      Source: '',
      Images: '',
    });
  }

  putContent(content: Content, fileToUpload: string[]) {
    const formData = new FormData();
    for (let i = 0; i < fileToUpload.length; ++i) {
      formData.append('Images', fileToUpload[i]);
    }
    formData.append('Id', content.Id.toString());
    formData.append('Title', content.Title);
    formData.append('Body', content.Body);
    formData.append('Source', content.Source);

    // place method for sending
  }
}
