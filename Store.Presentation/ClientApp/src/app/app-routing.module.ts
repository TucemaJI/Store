import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfirmPasswordComponent } from './modules/account/components/confirm-password/confirm-password.component';
import { PasswordRecoveryComponent } from './modules/account/components/password-recovery/password-recovery.component';
import { SignInComponent } from './modules/account/components/sign-in/sign-in.component';
import { SignUpComponent } from './modules/account/components/sign-up/sign-up.component';
import { ClientsComponent } from './modules/administrator/components/clients/clients.component';
import { AdministratorComponent } from './modules/administrator/components/administrator/administrator.component';
import { TestGuard } from './modules/shared/guards/test.guard';
import { HeaderComponent } from './modules/shared/header/header.component';
import { ProfileComponent } from './modules/user/profile/profile.component';

const routes: Routes = [

  { path: '', component: SignInComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'confirm-password', component: ConfirmPasswordComponent },
  { path: 'password-recovery', component: PasswordRecoveryComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'administrator', component: AdministratorComponent, canActivate: [TestGuard], children: [
      { path: 'clients', component: ClientsComponent }, ]
  },



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [TestGuard]
})
export class AppRoutingModule { }
