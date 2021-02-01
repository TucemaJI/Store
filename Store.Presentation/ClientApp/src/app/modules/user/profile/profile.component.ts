import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CheckerErrors } from '../../shared/validator';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;

  editbool: Boolean;

  constructor() { }

  ngOnInit(): void {
    this.profileForm = new FormGroup({
      firstName: new FormControl({value:'', disabled: !this.editbool}, [Validators.required]),
      lastName: new FormControl({value:'', disabled: !this.editbool}, [Validators.required]),
      email: new FormControl({value:'', disabled: !this.editbool}, [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.profileForm.controls[controlName].hasError(errorName);
  }

  public editButton (){
    this.editbool = true;
    CheckerErrors.edit(this.profileForm);
  }

}
