import { Component, Input, OnInit, inject } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { EmpleadoService } from '../../Services/empleado.service';
import { Router } from '@angular/router';
import { Empleado } from '../../Models/Empleado';


@Component({
  selector: 'app-empleado',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './empleado.component.html',
  styleUrl: './empleado.component.css'
})
export class EmpleadoComponent implements OnInit {

  @Input('id') idEmpleado! : number;
  private empleadoServicio = inject(EmpleadoService);
  public formBuild = inject(FormBuilder);

  public formEmpleado:FormGroup = this.formBuild.group({
    nombreCompleto: [''],
    direccion:[''],
    identificacion:[''],
    moneda:[''],
    fechaContrato:['']
  });

  constructor(private router:Router){}

  ngOnInit(): void {
    if(this.idEmpleado != 0){
      this.empleadoServicio.obtener(this.idEmpleado).subscribe({
        next:(data) =>{
          this.formEmpleado.patchValue({
            nombreDescripcion: data.nombreDescripcion,
            direccion:data.direccion,
            identificacion:data.identificacion,
            moneda:data.moneda,
            fechaCreacion:data.fechaCreacion
          })
        },
        error:(err) =>{
          console.log(err.message)
        }
      })
    }
  }

guardar(){
  const objeto : Empleado = {
    idEmpleado : this.idEmpleado,
    nombreDescripcion: this.formEmpleado.value.nombreDescripcion,
    direccion: this.formEmpleado.value.direccion,
    identificacion: this.formEmpleado.value.identificacion,
    moneda:this.formEmpleado.value.moneda,
    fechaCreacion:this.formEmpleado.value.fechaCreacion,
  }

  if(this.idEmpleado == 0){
    this.empleadoServicio.crear(objeto).subscribe({
      next:(data) =>{
        if(data.isSuccess){
          this.router.navigate(["/"]);
        }else{
          alert("Error al crear")
        }
      },
      error:(err) =>{
        console.log(err.message)
      }
    })
  }else{
    this.empleadoServicio.editar(objeto).subscribe({
      next:(data) =>{
        if(data.isSuccess){
          this.router.navigate(["/"]);
        }else{
          alert("Error al editar")
        }
      },
      error:(err) =>{
        console.log(err.message)
      }
    })
  }


}

volver(){
  this.router.navigate(["/"]);
}


}
