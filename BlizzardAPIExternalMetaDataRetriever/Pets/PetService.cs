using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class PetService : IPetService
    {
        private readonly IMediator _mediator;

        public PetService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Update(int id)
        {
            string result;
            try
            {
                await GetAndPersistPet(id);
                result = "Ok";
            }
            catch (Exception ex)
            {
                result = ex.Message + ex.StackTrace;
            }

            return result;
        }

        private async Task GetAndPersistPet(int id)
        {
            var pet = await _mediator.Send(new GetPetRequest { Id = id });
            TransformAndStore(pet);
        }

        public async Task<string> UpdateAll()
        {
            Stopwatch stopwatch = new Stopwatch();
            int currentPetId = -1;
            stopwatch.Start();

            var petsDownloaded = 0;

            try
            {
                List<int> petIds = new List<int>(await _mediator.Send(new GetPetIndexRequest()));

                foreach (var id in petIds)
                {
                    try
                    {
                        currentPetId = id;
                        await GetAndPersistPet(id);
                        petsDownloaded++;
                    }
                    catch (HttpRequestException)
                    {
                        // retry
                        await GetAndPersistPet(currentPetId);
                        petsDownloaded++;
                        continue;
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();

                        return
                            string.Format("After {0:hh\\:mm\\:ss} \r\n", stopwatch.Elapsed) +
                            string.Format("An error occurred when retrieving and persisting pet {0}\r\n", currentPetId)
                            + ex.Message.ToString()
                            + ex.StackTrace.ToString();
                    }
                }

                stopwatch.Stop();

                return string.Format("{0} pets downloaded in {1:hh\\:mm\\:ss}", petsDownloaded++, stopwatch.Elapsed);
            }
            catch (Exception)
            {
                // log error
                throw;
            }
        }

        internal void TransformAndStore(Pet pet)
        {
            // do something with the database here

            // _dataContext.SaveChanges();
        }
    }
} 
