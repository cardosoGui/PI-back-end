﻿using Domain.StoreContext.Enums;
using Shared.Entities;
using System;

namespace Domain.StoreContext.Entities
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;
        }

        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            //Se a Data estimada de entrega for no passado, n�o entregar.
            Status = EDeliveryStatus.Shipped;
        }

        public void Cancel()
        {
            //Se o Status for entregue, n�o pode cancelar.
            Status = EDeliveryStatus.Canceled;
        }
    }
}
