import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { MaterialModule } from '../shared/material.module';


@NgModule({
  declarations: [SignInComponent, SignUpComponent, RegistrationComponent],
  imports: [
    CommonModule,

    MaterialModule,

  ],
  exports: [SignInComponent, SignUpComponent, RegistrationComponent]
})
export class AccountModule { }
