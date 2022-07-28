using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatermemonSync.Repositoty;
using WatermemonSync.Request;
using System.IO;
using Newtonsoft.Json;

namespace WatermemonSync.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {
        private SyncRepository repository;

        private readonly ILogger<SyncController> _logger;

        public SyncController(ILogger<SyncController> logger, BlogDataContext context)
        {
            _logger = logger;
            repository = new SyncRepository(context);
        }

        [HttpGet]
        public PullResponse Get([FromQuery] long lastSyncAt)
        {
            if (lastSyncAt == null || lastSyncAt == 0)
                return new PullResponse() { Changes = repository.Pull(null), Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds() };
            return new PullResponse() { Changes = repository.Pull(DateTime.FromFileTimeUtc(lastSyncAt)), Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds() };

        }

        [HttpPost]
        public long Post([FromBody] PushRequest request)
        {
            var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            repository.Push(request.Changes);
            return time;
        }
    }
}
