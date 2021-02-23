import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { CheckerErrors } from 'src/app/modules/shared/validator';
import { IAppState } from 'src/app/store/state/app.state';
import { IChangeClientModel } from '../../../shared/models/IChangeClientModel';
import { IClients } from '../../../shared/models/IClients';
import { clientChange } from '../../store/administrator.actions';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  profileForm: FormGroup;


  constructor(@Inject(MAT_DIALOG_DATA) public data, private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.profileForm = new FormGroup({
      firstName: new FormControl(this.data.firstName, [Validators.required]),
      lastName: new FormControl(this.data.lastName, [Validators.required]),
      email: new FormControl(this.data.email, [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.profileForm.controls[controlName].hasError(errorName);
  }

  submit(profileFormValue) {
    let client: IChangeClientModel = {
      firstName: profileFormValue.firstName,
      lastName: profileFormValue.lastName,
      email: profileFormValue.email,
      password: profileFormValue.password,
      confirmPassword: profileFormValue.confirmPassword,
      id: this.data.id,
      isBlocked: this.data.isBlocked,
    };
    debugger;
    this.store.dispatch(clientChange({ client }));
    location.reload();
  }
}
