import { ComponentFixture, TestBed } from '@angular/core/testing';
import { QueueComponent } from './queue.component';
import { QueueFacade } from '../../state/queue-facade';

describe('QueueComponent', () => {
  let component: QueueComponent;
  let fixture: ComponentFixture<QueueComponent>;

  beforeEach(async () => {
    const fakeQueueFacade = {
      loading$: () => false,
      error$: () => null,
      loadData: () => {},
    };

    await TestBed.configureTestingModule({
      imports: [QueueComponent],
      providers: [{ provide: QueueFacade, useValue: fakeQueueFacade }],
    }).compileComponents();

    fixture = TestBed.createComponent(QueueComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
