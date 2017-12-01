﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialReaderConsole.Utils.ConsoleUtil;
using SerialReaderLibrary.Model;
using SerialReaderLibrary.Utils.TvShow;

namespace SerialReaderConsole
{
    public class ConsoleSeriesHandler
    {
        private readonly SeriesHandler _seriesHandler;

        public ConsoleSeriesHandler()
        {
            _seriesHandler = new SeriesHandler();
        }

        public ConsoleSeriesHandler(SeriesHandler seriesHandler)
        {
            _seriesHandler = seriesHandler;
        }


        /// <summary>
        /// Checks if series exists in allSeries collection, if not then downloads it from api.
        /// </summary>
        /// <param name="seriesName"></param>
        /// <returns></returns>
        public TvShow GetSeriesForConsole(string seriesName)
        {
            return _seriesHandler.GetSeries(seriesName);
        }

    }
}
