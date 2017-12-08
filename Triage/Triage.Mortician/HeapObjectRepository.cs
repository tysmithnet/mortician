﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Microsoft.Diagnostics.Runtime;
using Triage.Mortician.Abstraction;

namespace Triage.Mortician
{
    public class HeapObjectRepository : IHeapObjectRepository
    {
        protected internal Dictionary<ulong, IHeapObject> HeapObjects = new Dictionary<ulong, IHeapObject>();

        protected internal ILog Log = LogManager.GetLogger(typeof(HeapObjectRepository));

        public HeapObjectRepository(ClrRuntime runtime, List<IHeapObjectExtractor> heapObjectExtractors)
        {
            var heap = runtime.Heap;

            if(!heap.CanWalkHeap)
                Log.Debug("Heap is not walkable - unexpected results might arise");

            foreach (var obj in heap.EnumerateObjects().Where(x => !x.Type.IsFree && !x.IsNull))
            {                                                                                 
                foreach (var extractor in heapObjectExtractors)
                {
                    if (!extractor.CanExtract(obj, runtime)) continue;
                    try
                    {   
                        HeapObjects.Add(obj.Address, extractor.Extract(obj, runtime));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }  
            }

            foreach (var obj in heap.EnumerateObjects().Where(x => !x.Type.IsFree && !x.IsNull))
            {
                var parent = HeapObjects[obj.Address];

                foreach (var reference in obj.EnumerateObjectReferences())
                {   
                    if(HeapObjects.TryGetValue(reference.Address, out IHeapObject child))
                        parent.AddReference(child);
                    else
                        Log.Warn($"{parent.Address:x} has a reference to {reference.Address:x}, but it was not found in the heap cache");
                }    
            }
        }

        public IHeapObject Get(ulong address)
        {
            if (HeapObjects.TryGetValue(address, out IHeapObject obj))
                return obj;
            throw new IndexOutOfRangeException($"There is no object matching address: {address:x}");
        }   
    }
}