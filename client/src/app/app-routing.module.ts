import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrrorComponent } from './errors/server-errror/server-errror.component';
import { AuthGuard } from './guards/auth.guard';
import { PreventUnsavedChangesGuard } from './guards/prevent-unsaved-changes.guard';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';

const routes: Routes = [
  {
    path:"",
    component:HomeComponent
  },

  {
    path:"",
    runGuardsAndResolvers:"always",
    children:[
      {
        path:"members",
        component:MemberListComponent
      },
      {
        path:"member/edit",
        component:MemberEditComponent,
        canDeactivate:[PreventUnsavedChangesGuard]
      },
      {
        path:"member/:username",
        component:MemberDetailComponent
      },
      {
        path:"lists",
        component:ListsComponent
      },
      {
        path:"messages",
        component:MessagesComponent
      },
    ]
  },
  {
    path:"not-found",
    component:NotFoundComponent
  },
  {
    path:"server-error",
    component:ServerErrrorComponent
  },
  {
    path:"**",
    component:HomeComponent,
    pathMatch:"full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
