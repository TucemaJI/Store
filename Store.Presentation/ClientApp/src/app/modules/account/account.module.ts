import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { MaterialModule } from '../shared/material.module';
import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { ConfirmPasswordComponent } from './components/confirm-password/confirm-password.component';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmPasswordComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,

  ],
  exports: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmPasswordComponent]

})
export class AccountModule { }
