import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { editUser, getUser } from '../../account/store/account.actions';
import { selectUser } from '../../printing-edition/store/printing-edition.selector';
import { User } from '../../shared/models/User';
import { AuthService } from '../../shared/services/auth.service';
import { CheckerErrors } from '../../shared/validator';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;

  user: User;
  editbool: Boolean;
  userId: string;

  constructor(private auth: AuthService, private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);

    this.store.dispatch(getUser({ userId: this.userId }));
    this.getUserForm();
    this.profileForm = new FormGroup({
      firstName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required]),
      lastName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required]),
      email: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });
  }

  getUserForm() {
    this.store.pipe(select(selectUser)).subscribe(

      data => {
        if (data.id != null) {
          this.user = data;

          console.log(data);
        }
      }
    )
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.profileForm.controls[controlName].hasError(errorName);
  }

  public editButton() {
    this.editbool = true;
    CheckerErrors.edit(this.profileForm);
  }

  public cancel() {
    location.reload();
  }

  public save(profileFormValue) {
    console.log(profileFormValue);
    const eUser: User = {
      accessToken: this.user.accessToken,
      confirmPassword: profileFormValue.confirmPassword,
      confirmed: this.user.confirmed,
      email: profileFormValue.email,
      firstName: profileFormValue.firstName,
      id: this.user.id,
      lastName: profileFormValue.lastName,
      password: profileFormValue.password,
      refreshToken: this.user.refreshToken,
    }

    this.store.dispatch(editUser({ user: eUser }));
    debugger;
  }

}
