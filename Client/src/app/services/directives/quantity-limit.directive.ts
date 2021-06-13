import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator, ValidatorFn } from '@angular/forms';

export const quantityLimitExceededValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const quantity = control.get('quantity');
    const quantityLimit = control.get('quantityLimit');
  
    return quantity && quantityLimit && quantity.value < quantityLimit.value ? { quantityLimitExceeded: true } : null;
  };

@Directive({
  selector: '[appQantityLimit]',
  providers: [{ provide: NG_VALIDATORS, useExisting: QuantityLimitDirective, multi: true }]
})
export class QuantityLimitDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
    return quantityLimitExceededValidator(control);
  }
}



