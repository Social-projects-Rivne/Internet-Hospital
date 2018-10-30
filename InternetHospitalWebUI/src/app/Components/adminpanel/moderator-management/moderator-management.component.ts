import { Component, OnInit, ViewChild } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';

import { EditModeratorService } from '../Services/edit-moderator.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-moderator-management',
  templateUrl: './moderator-management.component.html',
  styleUrls: ['./moderator-management.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.74, 0.97, 0.91, 1)')),
    ]),
  ],
})
export class ModeratorManagementComponent implements OnInit {
  dataSource: MatTableDataSource<UserData>;
  selection = new SelectionModel<UserData>(true, []);
  displayedColumns = ['select', 'id', 'email', 'surname', 'name', 'lastname', 'status', 'edit'];
  dataColumns = ['id', 'email', 'surname', 'name', 'lastname', 'status'];
  displayedRow: UserData;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private service: EditModeratorService, private router: Router){
    const users: UserData[] = [];
    for (let i = 1; i <= 100; i++) {
        users.push(createNewUser(i));
    }
    //Assign the data to the data source for the table to render
    
    this.dataSource = new MatTableDataSource(users);
  }

  ngOnInit(){
    this.dataSource.paginator = this.paginator;
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }
  
  // Selects all rows if they are not all selected; otherwise clear selection. 
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  changeRow(row)
  {  
    this.displayedRow = row === this.displayedRow ? null : row;
  }

  editModer(form: NgForm)
  {
    //place for calling editing service
    if(this.service.form.valid) {
      this.service.putModer(form.value).subscribe(res => console.log(res));      
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
    else
    {
      this.service.form.reset();
      this.service.initializeFormGroup();
    }
  }
 

  createNewModer()
  {
    this.router.navigate(['/createmod']);
  }
}

//Temp fynctional for generating data

// Constants used to fill up our data base. 
const SURNAMES = [
  'Cook',
  'Smith',
  'Fuelk',
  'Stam',
  'Hill',
  'Gradje',
  'Mikj',
  'Vise',
  'Lake',
  'Nert',
  'Malt',
  'Gobs',
  'Fryder',
  'Mankohen',
  'Cherw'
];
const NAMES = [
  'Maia',
  'Asher',
  'Olivia',
  'Atticus',
  'Amelia',
  'Jack',
  'Charlotte',
  'Theodore',
  'Isla',
  'Oliver',
  'Isabella',
  'Jasper',
  'Cora',
  'Levi',
  'Violet',
  'Arthur',
  'Mia',
  'Thomas',
  'Elizabeth'
];

export interface UserData {
  id: string;
  email:string;
  name: string;
  surname: string;
  lastname: string;
  status: string;
}

//Builds and returns a new User. 
function createNewUser(id: number): UserData {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))];
  const surname = SURNAMES[Math.round(Math.random() * (SURNAMES.length - 1))];
  const lastname = NAMES[Math.round(Math.random() * (NAMES.length - 1))];
  const email = surname.substr(0, Math.round(Math.random() * surname.length)) 
                + name.substr(0, Math.round(Math.random() * name.length)) 
                + "@gmail.com";

  return {
      id: id.toString(),
      surname: surname,
      name: name,
      lastname: lastname,
      email: email,
      status: Math.round(Math.random()*1)?"active":"deleted"
  };
}
