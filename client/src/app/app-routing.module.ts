import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminMainComponent } from './admin/admin-main/admin-main.component';
import { AgentMainComponent } from './agent/agent-main/agent-main.component';
import { LoginComponent } from './login/login.component';
import { VisitorMainComponent } from './visitor/visitor-main/visitor-main.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminMainComponent, canActivate: [AuthGuard] },
  { path: 'agent', component: AgentMainComponent, canActivate: [AuthGuard] },
  { path: 'visitor', component: VisitorMainComponent, canActivate:[AuthGuard] },
  { path: '**', component: LoginComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
