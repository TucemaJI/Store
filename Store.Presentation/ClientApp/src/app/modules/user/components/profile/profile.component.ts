import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { editUser, getUser } from '../../../account/store/account.actions';
import { selectUser } from '../../../printing-edition/store/printing-edition.selector';
import { IUser } from '../../../shared/models/IUser.model';
import { AuthService } from '../../../shared/services/auth.service';
import { CheckerErrors } from '../../../shared/validator';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;

  user: IUser;
  editbool: Boolean;
  userId: string;

  constructor(private auth: AuthService, private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);

    this.store.dispatch(getUser({ userId: this.userId }));
    this.getUserForm();
    this.profileForm = new FormGroup({
      firstName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, CheckerErrors.checkFirstSpace]),
      lastName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, CheckerErrors.checkFirstSpace]),
      email: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });
  }

  getUserForm(): void {
    this.store.pipe(select(selectUser)).subscribe(

      data => {
        if (data != null) {
          this.user = data;
        }
      }
    )
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.profileForm.controls[controlName].hasError(errorName);
  }

  public editButton(): void {
    this.editbool = true;
    CheckerErrors.edit(this.profileForm);
  }

  public cancel(): void {
    location.reload();
  }

  public save(profileFormValue: IUser): void {
    const user: IUser = {
      confirmPassword: profileFormValue.confirmPassword,
      email: profileFormValue.email,
      firstName: profileFormValue.firstName.trim(),
      id: this.user.id,
      lastName: profileFormValue.lastName.trim(),
      password: profileFormValue.password,
    }
    this.store.dispatch(editUser({ user }));
    this.editbool = false;
  }

}
