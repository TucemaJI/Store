import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { MaterialModule } from '../shared/material.module';
import { PasswordRecoveryComponent } from './components/password-recovery/password-recovery.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmEmailComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
  ],
  exports: [SignInComponent, SignUpComponent, PasswordRecoveryComponent, ConfirmEmailComponent]
})

export class AccountModule { }
