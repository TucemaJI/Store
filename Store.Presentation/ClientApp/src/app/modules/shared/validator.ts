import { FormControl, FormGroup, FormGroupDirective, NgForm } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control?.invalid && control?.parent?.touched);
    const invalidParent = !!(control?.parent?.invalid && control?.parent?.touched);

    return (invalidCtrl || invalidParent);
  }
}

export class CheckerErrors {

  public static checkUpperCase(formControl: FormControl) {
    let result = formControl.value.match(/[A-Z]/);
    if (result == null && formControl.value != "") {
      return { notHaveUpper: true };
    }
    return null;
  }

  public static checkSpecialSymbol(formControl: FormControl) {
    let result = formControl.value.match(/[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/);
    if (result == null && formControl.value != "") {
      return { notHaveSpecialSymbol: true };
    }
    return null;
  }

  public static checkLength(formControl: FormControl) {
    if (formControl.value.length >= 6 || formControl.value == "") {
      return null;
    }
    return { notHaveLength: true };
  }

  public static checkNum(formControl: FormControl) {
    let result = formControl.value.match(/[0-9]/);
    if (result == null && formControl.value != "") {
      return { notHaveNum: true };
    }
    return null;
  }

  public static checkPasswords(group: FormGroup) {
    const password = group.get('password').value;
    const confirmPassword = group.get('confirmPassword').value;

    return password === confirmPassword ? null : { notSame: true }
  }

  public static edit (form: FormGroup){
    form.get('firstName').enable();
    form.get('lastName').enable();
    form.get('email').enable();
  }
}