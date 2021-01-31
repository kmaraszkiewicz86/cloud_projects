import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'

import { DynamoDbListComponent } from './dynamo-db-list/dynamo-db-list.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'photo-list-aws', component: DynamoDbListComponent },
      { path: '', redirectTo: 'photo-list-aws', pathMatch: 'full' },
      { path: '**', redirectTo: 'photo-list-aws', pathMatch: 'full'}
    ])
  ],
  declarations: [
    DynamoDbListComponent
  ]
})
export class PhotoAwsModule { }
