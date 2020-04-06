﻿// Copyright (c) Damir Dobric. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoCortexApi.Entities
{

    /// <summary>
    /// Helper entity for column bursting.
    /// </summary>
    public class BurstingResult
    {
        
        public BurstingResult(IList<Cell> cells, Cell bestCell)
        {
            this.Cells = cells;
            this.BestCell = bestCell;
        }

        /// <summary>
        ///  List of the processed column's cells.
        /// </summary>
        public IList<Cell> Cells { get; set; }


        /// <summary>
        /// Best rated cell.
        /// </summary>
        public Cell BestCell { get; set; }
    }
}
