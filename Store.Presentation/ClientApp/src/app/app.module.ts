import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountModule } from './modules/account/account.module';
import { CartModule } from './modules/cart/cart.module';
import { OrderModule } from './modules/order/order.module';
import { PrintingEditionModule } from './modules/printing-edition/printing-edition.module';
import { SharedModule } from './modules/shared/shared.module';
import { UserModule } from './modules/user/user.module';
import { MaterialModule } from './modules/shared/material.module';
import { ToastNoAnimationModule } from 'ngx-toastr';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AccountHttpService } from './modules/shared/services/account-http.service';
import { TokenInterceptor } from './modules/shared/interceptors/auth.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthService } from './modules/shared/services/auth.service';
import { ErrorInterceptor } from './modules/shared/interceptors/error.interceptor';
import { NgxPaginationModule } from 'ngx-pagination';
import { PrintingEditionHttpService } from './modules/shared/services/printing-edition-http.service';
import { ShoppingCartModule } from 'ng-shopping-cart';
import { ShoppingCartService } from './modules/shared/services/shopping-cart.service';
import { CookieService } from 'ngx-cookie-service';
import { OrderHttpService } from './modules/shared/services/order-http.service';
import { Consts } from './modules/shared/consts';
import { NgxsModule } from '@ngxs/store';
import { NgxsReduxDevtoolsPluginModule } from '@ngxs/devtools-plugin';
import { NgxsLoggerPluginModule } from '@ngxs/logger-plugin';
import { AccountState } from './modules/account/store/account.state';
import { CartState } from './modules/cart/store/cart.state';
import { OrderState } from './modules/order/store/order.state';
import { PrintingEditionsState } from './modules/printing-edition/store/printing-edition.state';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    NgxsModule.forRoot([
      AccountState,
      CartState,
      OrderState,
      PrintingEditionsState,
    ], { developmentMode: !environment.production }),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgxsLoggerPluginModule.forRoot(),
    ToastNoAnimationModule.forRoot(),
    NgxPaginationModule,
    ShoppingCartModule.forRoot(),
    MaterialModule,
    AccountModule,
    CartModule,
    OrderModule,
    PrintingEditionModule,
    SharedModule,
    UserModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem(Consts.ACCESS_TOKEN);
        }
      }
    }),
  ],
  providers: [AccountHttpService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    PrintingEditionHttpService,
    ShoppingCartService,
    CookieService,
    OrderHttpService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
