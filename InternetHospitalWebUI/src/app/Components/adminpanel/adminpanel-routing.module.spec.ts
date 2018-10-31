import { AdminpanelRoutingModule } from './adminpanel-routing.module';

describe('AdminpanelRoutingModule', () => {
  let adminpanelRoutingModule: AdminpanelRoutingModule;

  beforeEach(() => {
    adminpanelRoutingModule = new AdminpanelRoutingModule();
  });

  it('should create an instance', () => {
    expect(adminpanelRoutingModule).toBeTruthy();
  });
});
