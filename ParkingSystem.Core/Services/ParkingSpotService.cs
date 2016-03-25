﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Core.Utils;
using ParkingSystem.Core.Models;

namespace ParkingSystem.Core.Services
{
    public interface IParkingSpotService
    {
        IList<ParkingSpot> GetAllParkingSpots();
        PagedParkingSpots GetParkingSpots(PagingInfo pagination);
        ParkingSpot GetParkingSpot(int id);

        bool TestIfParkingSpotWithSameNameAlreadyExists(ParkingSpot parkingSpot);

        void AddParkingSpot(ParkingSpot parkingSpot);
        void UpdateParkingSpot(ParkingSpot parkingSpot);
        void DeleteParkingSpot(int id);
    }

    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParkingSpotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<ParkingSpot> GetAllParkingSpots()
        {
            return _unitOfWork.ParkingSpots.GetAll().ToList();
        }

        public PagedParkingSpots GetParkingSpots(PagingInfo pagination)
        {
            return _unitOfWork.ParkingSpots.GetAll(pagination);
        }

        public ParkingSpot GetParkingSpot(int id)
        {
            return _unitOfWork.ParkingSpots.Get(id);
        }
        
        public bool TestIfParkingSpotWithSameNameAlreadyExists(ParkingSpot parkingSpot)
        {
            var foundParkingSpots = _unitOfWork.ParkingSpots
                .Find(p => p.Name == parkingSpot.Name && p.Id != parkingSpot.Id);

            return foundParkingSpots.Count() > 0;
        }

        public void AddParkingSpot(ParkingSpot parkingSpot)
        {
            _unitOfWork.ParkingSpots.Add(parkingSpot);
            _unitOfWork.SaveChanges();
        }

        public void DeleteParkingSpot(int id)
        {
            var parkingSpot = _unitOfWork.ParkingSpots.Get(id);

            if (parkingSpot != null)
            {
                _unitOfWork.ParkingSpots.Remove(parkingSpot);
                _unitOfWork.SaveChanges();
            }
        }

        public void UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            var originalParkingSpot = _unitOfWork.ParkingSpots.Get(parkingSpot.Id);

            if (originalParkingSpot != null)
            {
                originalParkingSpot.Name = parkingSpot.Name;
                originalParkingSpot.Type = parkingSpot.Type;
                _unitOfWork.SaveChanges();
            }
        }
    }
}
