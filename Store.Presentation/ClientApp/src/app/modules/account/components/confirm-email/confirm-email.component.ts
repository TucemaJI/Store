import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { IConfirm } from '../../../shared/models/IConfirm.model';
import { confirmEmail as confirmEmail } from '../../store/account.actions';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  fromLink: boolean;
  mail: string;
  token: string;
  name: string;
  lName: string;
  confirmModel: IConfirm;
  password: string;
  constructor(private store: Store, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe(params => {
      this.mail = params.mail;
      this.token = decodeURIComponent(params.token);
      this.name = params.name;
      this.lName = params.lName;
      this.password = params.pass;
      this.confirmModel = { email: this.mail, token: this.token, password: this.password }
      if (this.mail !== undefined) {
        this.fromLink = true;
      }
    });
  }
  
  public submit(confirmModel: IConfirm): void {
    this.store.dispatch(confirmEmail({ confirmModel }));
  }
}
