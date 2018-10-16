import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

enum TypeCodes{
  PNG="89504e470d0a1a0a",
  JPG="ffd8ff",
}

@Injectable({
  providedIn: 'root'
})
export class ImageValidationService {

  constructor(private http: HttpClient) { }

  private BASE64_MARKER:string = ';base64,';
  private AMOUNT_OF_BYTES_FOR_CHECK = 16;
  private SYSTEM_OF_NUMBERS = 16;

  private convertDataURIToBinary(dataURI): Uint8Array {  
    var base64Index = dataURI.indexOf(this.BASE64_MARKER) + this.BASE64_MARKER.length;
    var base64 = dataURI.substring(base64Index);
    var raw = window.atob(base64);
    var array = new Uint8Array(new ArrayBuffer(raw.length));
    for(var i = 0; i < raw.length; i++) {
      array[i] = raw.charCodeAt(i);
    }
    return array;
  }

  private getImageHeader(img: Uint8Array): string {
    var header: string = "";
    for(var i = 0; i < img.length; i++) {
       header += img[i].toString(this.SYSTEM_OF_NUMBERS);
    }
    return header;
  }

  isImageFile(uri): boolean {
    var isImg = false;
    var arr = this.convertDataURIToBinary(uri).subarray(0, this.AMOUNT_OF_BYTES_FOR_CHECK);
    var header = this.getImageHeader(arr);
    if(header.slice(0, TypeCodes.JPG.length) === TypeCodes.JPG
        || header.slice(0, TypeCodes.PNG.length) === TypeCodes.PNG){
      isImg = true;
    }
    return isImg;
  }

  hasImageValidSize(maxHeight, maxWidth, minHeight, minWidth, height, width): boolean {
    var isNormalHeight = height < maxHeight && height > minHeight;
    var isNormalWidth = width < maxWidth && width > minWidth;
    return isNormalHeight && isNormalWidth;
  }
}
