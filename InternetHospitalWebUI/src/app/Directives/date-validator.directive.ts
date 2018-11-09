import { Directive } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { PASSPORT_AGE } from '../config';

@Directive({
  selector: '[appDateValidator]'
})
export class DateValidatorDirective {

  constructor() { }

}

export function MaxDateValidator(control: AbstractControl): { [key: string]: boolean } | null {
  console.log("FROM DIRECTIVE: " + control);
  
  let dateRequirement = new Date().getFullYear() - new Date(control.value).getFullYear();
  if (control.value == null || dateRequirement < PASSPORT_AGE) {
    return { 'date': true }
  }
  return null;
}
