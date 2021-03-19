import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CartComponent } from '../../cart/componensts/cart/cart.component';
import { Consts } from '../consts';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  userId: string;

  constructor(public dialog: MatDialog, private auth: AuthService) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
  }

  logOut(): void {
    localStorage.clear();
  }

  cart(): void {
    const dialog = this.dialog.open(CartComponent).updateSize(Consts.CART_DIALOG_SIZE);
  }

}
