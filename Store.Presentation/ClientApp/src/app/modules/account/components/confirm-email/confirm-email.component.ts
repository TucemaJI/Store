import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngxs/store';
import { IConfirm } from '../../../shared/models/IConfirm.model';
import { ConfirmEmail } from '../../store/account.actions';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  public fromLink: boolean;
  public mail: string;
  public token: string;
  public name: string;
  public lName: string;
  public confirmModel: IConfirm;
  public password: string;
  constructor(private store: Store, private activateRoute: ActivatedRoute) { }
  
  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe(params => {
      this.mail = params.mail;
      this.token = decodeURIComponent(params.token);
      this.name = params.name;
      this.lName = params.lName;
      this.password = params.pass;
      this.confirmModel = { email: this.mail, token: this.token }
      if (this.mail !== undefined) {
        this.fromLink = true;
      }
    });
  }

  public submit(): void {
    this.store.dispatch(new ConfirmEmail({ model: this.confirmModel }));
  }
}
