import { Component, OnInit } from '@angular/core';
import { Content } from '../../../Models/Content';


@Component({
  selector: 'app-content-management',
  templateUrl: './content-management.component.html',
  styleUrls: ['./content-management.component.scss']
})
export class ContentManagementComponent implements OnInit {

  selectedContent: Content = null;
  contentItems: Content[] = [];

  constructor() {
  }

  ngOnInit() {
    for (let i = 0; i < 5; ++i) {
      this.contentItems.push(createNewContent(i));
    }
  }

  onChange(sel: Content) {
    this.selectedContent = sel;
  }

  onDelete(sel: Content) {
    const ind = this.contentItems.indexOf(sel);
    this.contentItems.splice(ind, 1);

    // method for delete from DB
  }

}

// Generating data
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

// Builds and returns a new Content.
function createNewContent(id: number): Content {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))];
  const surname = SURNAMES[Math.round(Math.random() * (SURNAMES.length - 1))];
  const lastname = NAMES[Math.round(Math.random() * (NAMES.length - 1))];
  const email = surname.substr(0, Math.round(Math.random() * surname.length))
                + name.substr(0, Math.round(Math.random() * name.length))
                + '@gmail.com';

  const cont: Content = new Content();
  cont.Id = id;
  cont.Title = email;
  cont.Source = lastname;
  cont.Images = [ 'https://whitehousepawprints.com/wp-content/uploads/2017/05/family-2.jpg',
                  'https://www.maritimefirstnewspaper.com/wp-content/uploads/2018/07/family-3.jpg',
                  'https://vanierinstitute.ca/wp-content/uploads/2016/05/Diversity-diversit%C3%A9.jpg'];
  cont.Body = '';
  for (let i = 0; i < 100; ++i) {
    cont.Body += 'wwwwwwwwww';
  }
  return cont;
}

