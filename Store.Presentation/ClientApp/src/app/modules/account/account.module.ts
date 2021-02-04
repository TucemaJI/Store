import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { MaterialModule } from '../shared/material.module';
import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { ConfirmPasswordComponent } from './components/confirm-password/confirm-password.component';
import { RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import * as fromAccount from './store/account.reducer'


@NgModule({
  declarations: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmPasswordComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    StoreModule.forFeature(fromAccount.reducerKey, fromAccount.reducer),

  ],
  exports: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmPasswordComponent]

})
export class AccountModule { }
