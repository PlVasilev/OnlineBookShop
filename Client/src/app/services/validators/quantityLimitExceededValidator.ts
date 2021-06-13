import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const quantityLimitExceededValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const quantity = control.get('quantity');
    const quantityLimit = control.get('quantityLimit');
  
    return quantity && quantityLimit && quantity.value < quantityLimit.value ? { quantityLimitExceeded: true } : null;
  };