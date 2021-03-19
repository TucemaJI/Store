import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfirmEmailComponent } from './modules/account/components/confirm-email/confirm-email.component';
import { PasswordRecoveryComponent } from './modules/account/components/password-recovery/password-recovery.component';
import { SignInComponent } from './modules/account/components/sign-in/sign-in.component';
import { SignUpComponent } from './modules/account/components/sign-up/sign-up.component';
import { ClientsComponent } from './modules/administrator/components/clients/clients.component';
import { AdministratorComponent } from './modules/administrator/components/administrator/administrator.component';
import { AdminGuard } from './modules/shared/guards/admin.guard';
import { ProfileComponent } from './modules/user/profile/profile.component';
import { AuthorsComponent } from './modules/administrator/components/authors/authors.component';
import { PrintingEditionComponent } from './modules/printing-edition/components/printing-edition/printing-edition.component';
import { SelectPEComponent } from './modules/printing-edition/components/select-pe/select-pe.component';
import { AuthorizeGuard } from './modules/shared/guards/authorize.guard';
import { OrdersComponent } from './modules/order/components/orders/orders.component';

const routes: Routes = [

  { path: '', component: PrintingEditionComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'password-recovery', component: PasswordRecoveryComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthorizeGuard] },
  {
    path: 'administrator', component: AdministratorComponent, canActivate: [AdminGuard], children: [
      { path: 'clients', component: ClientsComponent },
      { path: 'authors', component: AuthorsComponent },]
  },
  { path: 'printing-edition', component: PrintingEditionComponent },
  { path: 'pe/:id', component: SelectPEComponent },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AdminGuard, AuthorizeGuard]
})
export class AppRoutingModule { }
