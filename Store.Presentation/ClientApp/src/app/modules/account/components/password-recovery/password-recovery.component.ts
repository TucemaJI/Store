import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { passwordRecovery } from '../../store/account.actions';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {
  emailForm: FormGroup;
  passwordSent: boolean;

  constructor(private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.emailForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.emailForm.controls[controlName].hasError(errorName);
  }

  public continue(email: { email: string }): void {
    if (this.emailForm.valid) {
      this.store.dispatch(passwordRecovery(email));
      this.passwordSent = true;
    }
  }

}
