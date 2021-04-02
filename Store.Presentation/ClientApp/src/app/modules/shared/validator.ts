import { FormControl, FormGroup, FormGroupDirective, NgForm } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";
import { Consts } from "./consts";

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control?.invalid && control?.parent?.touched);
    const invalidParent = !!(control?.parent?.invalid && control?.parent?.touched);

    return (invalidCtrl || invalidParent);
  }
}

export class CheckerErrors {

  public static checkFirstSpace(formControl: FormControl) {
    if(formControl.value.length !== formControl.value.trim().length){
      return { firstSpace: true };
    }
    return null;
  }

  public static checkUpperCase(formControl: FormControl) {
    let result = formControl.value.match(Consts.VALID_LETTERS);
    if (result == null && formControl.value != "") {
      return { notHaveUpper: true };
    }
    return null;
  }

  public static checkSpecialSymbol(formControl: FormControl) {
    let result = formControl.value.match(Consts.VALID_SYMBOLS);
    if (result == null && formControl.value != "") {
      return { notHaveSpecialSymbol: true };
    }
    return null;
  }

  public static checkLength(formControl: FormControl) {
    if (formControl.value.length >= Consts.VALID_MIN_LENGTH || formControl.value == "") {
      return null;
    }
    return { notHaveLength: true };
  }

  public static checkNum(formControl: FormControl) {
    let result = formControl.value.match(Consts.VALID_NUM);
    if (result == null && formControl.value != "") {
      return { notHaveNum: true };
    }
    return null;
  }

  public static checkPasswords(group: FormGroup) {
    const password = group.get(Consts.PASSWORD).value;
    const confirmPassword = group.get(Consts.CONFIRM_PASSWORD).value;

    return password === confirmPassword ? null : { notSame: true }
  }

  public static edit(form: FormGroup) {
    form.get(Consts.FIRST_NAME).enable();
    form.get(Consts.LAST_NAME).enable();
    form.get(Consts.EMAIL).enable();
  }
}