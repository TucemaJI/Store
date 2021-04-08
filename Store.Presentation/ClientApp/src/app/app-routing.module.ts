import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfirmEmailComponent } from './modules/account/components/confirm-email/confirm-email.component';
import { PasswordRecoveryComponent } from './modules/account/components/password-recovery/password-recovery.component';
import { SignInComponent } from './modules/account/components/sign-in/sign-in.component';
import { SignUpComponent } from './modules/account/components/sign-up/sign-up.component';
import { ProfileComponent } from './modules/user/components/profile/profile.component';
import { PrintingEditionComponent } from './modules/printing-edition/components/printing-edition/printing-edition.component';
import { SelectPEComponent } from './modules/printing-edition/components/select-printing-edition/select-printing-edition.component';
import { AuthorizeGuard } from './modules/shared/guards/authorize.guard';
import { OrdersComponent } from './modules/order/components/orders/orders.component';
import { Consts } from './modules/shared/consts';

const routes: Routes = [

  { path: '', component: PrintingEditionComponent },
  { path: Consts.ROUTE_SIGN_IN, component: SignInComponent },
  { path: Consts.ROUTE_SIGN_UP, component: SignUpComponent },
  { path: Consts.ROUTE_CONFIRM_EMAIL, component: ConfirmEmailComponent },
  { path: Consts.ROUTE_PASSWORD_RECOVERY, component: PasswordRecoveryComponent },
  { path: Consts.ROUTE_PROFILE, component: ProfileComponent, canActivate: [AuthorizeGuard] },
  { path: Consts.ROUTE_PRINTING_EDITIONS, component: PrintingEditionComponent },
  { path: Consts.ROUTE_PRINTING_EDITION, component: SelectPEComponent },
  { path: Consts.ROUTE_ORDERS, component: OrdersComponent, canActivate: [AuthorizeGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthorizeGuard]
})
export class AppRoutingModule { }
