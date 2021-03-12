import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { CartComponent } from '../../cart/componensts/cart/cart.component';
import { selectUser } from '../../printing-edition/store/printing-edition.selector';
import { User } from '../models/User';
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

  cart() {
    const dialog = this.dialog.open(CartComponent).updateSize("300%");
  }

}
