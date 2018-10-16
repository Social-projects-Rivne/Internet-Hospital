import { Directive, Input } from '@angular/core';
import { Validator, AbstractControl, ValidationErrors, NG_VALIDATORS, ValidatorFn } from '@angular/forms';
import { Subscription } from "rxjs";

export function compareValidator(controlNameTocompare: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    if (control.value === null || control.value.length === 0) {
      return null;
    }
    const CONTROL_TO_COMPARE = control.root.get(controlNameTocompare);
    if (CONTROL_TO_COMPARE) {
      const SUBSCRIPTION: Subscription = CONTROL_TO_COMPARE.valueChanges.subscribe(() => {
        control.updateValueAndValidity();
        SUBSCRIPTION.unsubscribe();
      });
    }

    return CONTROL_TO_COMPARE && CONTROL_TO_COMPARE.value !== control.value ? { 'compare': true } : null;
  }
};

@Directive({
  selector: '[compare]',
  providers: [{ provide: NG_VALIDATORS, useExisting: CompareValidatorDirective, multi: true }]
})
export class CompareValidatorDirective implements Validator {
  @Input('compare') controlNameToCompare: string;

  validate(control: AbstractControl): ValidationErrors | null {
   return compareValidator(this.controlNameToCompare)(control);
  }
}
