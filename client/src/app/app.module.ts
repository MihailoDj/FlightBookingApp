import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { AgentMainComponent } from './agent/agent-main/agent-main.component';
import { VisitorMainComponent } from './visitor/visitor-main/visitor-main.component';
import { UserControlsComponent } from './user-controls/user-controls.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { FlightModalComponent } from './flight-modal/flight-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminMainComponent,
    AgentMainComponent,
    VisitorMainComponent,
    UserControlsComponent,
    FlightModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    TabsModule,
  ],
  providers: [
    BsModalService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
