import { Component, OnInit, Input } from '@angular/core';
import { NgForm } from '@angular/forms';

import { Content } from '../../../../Models/Content';
import { ImageValidationService } from '../../../../services/image-validation.service';
import { EditContentService } from '../../services/edit-content.service';
import { CreateContentService } from '../../services/create-content.service';

const MAX_WIDTH_IMG = 2000;
const MAX_HEIGHT_IMG = 2000;
const MIN_WIDTH_IMG = 300;
const MIN_HEIGHT_IMG = 300;

@Component({
  selector: 'app-content-edit',
  templateUrl: './content-edit.component.html',
  styleUrls: ['./content-edit.component.scss']
})
export class ContentEditComponent implements OnInit {

  constructor(private service: EditContentService, private imgService: ImageValidationService) { }

  ngOnInit() {
  }

  @Input() content: Content;
  imgs: File[] = null;


  onClear() {
    this.service.form.reset();
    this.content = null;
  }

  handleFileInput(file : FileList){
    this.imgs = [];
    for(var i = 0; i < file.length && i < 5; ++i) {
      var reader = new FileReader();

      reader.onload = (event: any) => {
        if(event.target.readyState === FileReader.DONE) {
          if (this.imgService.isImageFile(event.target.result) == false ) {
            // notification service will replace this alert in future
            alert("Only image files are acceptable!");
          }
          else{
            var img = new Image();
            
            img.onload = () => {
              if(this.imgService.hasImageValidSize(MAX_HEIGHT_IMG, MAX_WIDTH_IMG, 
                                                    MIN_HEIGHT_IMG, MIN_WIDTH_IMG, 
                                                    img.height, img.width)) {
                this.imgs.push(event.target.result);
              }
              else {
                this.service.form.invalid;
                // notification service will replace this alert in future
                alert("Image is invalid! It might be too big or too small.");
              }
            }
            img.src = event.target.result;
          }
        }
      }
      reader.readAsDataURL(file.item(i));
    }
  }

  onSubmit(form: NgForm) {
    if(this.service.form.valid) {
     // this.service.putContent(form.value, this.fileToUpload).subscribe(res => console.log(res)); 
    }     
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.content = null;
  }

}
