import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { IAppState } from 'src/app/store/state/app.state';
import { refreshToken } from '../../account/store/account.actions';
import { CartComponent } from '../../cart/componensts/cart/cart.component';
import { Consts } from '../consts';
import { AuthService } from '../services/auth.service';
import { ShoppingCartService } from '../services/shopping-cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  userId: string;
  count: number;

  constructor(public dialog: MatDialog, private auth: AuthService, private router: Router, private store: Store<IAppState>, private cartService: ShoppingCartService<BaseCartItem>) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    this.count = this.cartService.getItems().length;
    this.cartService.cartChanged.subscribe((count) => this.count = count);
  }

  logOut(): void {
    localStorage.clear();
    location.reload();
  }

  cart(): void {
    const dialog = this.dialog.open(CartComponent).updateSize(Consts.CART_DIALOG_SIZE);
  }

  check(): void {
    if (this.userId === undefined) {
      this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
    }
    if (!this.auth.isAuthenticated()) {
      const token = this.auth.getTokens();
      this.store.dispatch(refreshToken({ token }));
    }
  }
}
