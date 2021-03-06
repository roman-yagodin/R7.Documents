﻿using System.Collections;
using R7.Documents.Models;

namespace R7.Documents.ViewModels
{
    public class DocumentViewModelComparer : IComparer
    {
        ArrayList mobjSortColumns;

        public DocumentViewModelComparer (ArrayList sortColumns)
        {
            mobjSortColumns = sortColumns;
        }

        /// <summary>
        /// Compares two documents and returns a value indicating whether one is less than,
        /// equal to, or greater than the other. This method is of Comparison&lt;T&gt; delegate type
        /// </summary>
        /// <param name="x">First document.</param>
        /// <param name="y">Second document.</param>
        public int Compare (IDocumentViewModel x, IDocumentViewModel y)
        {
            if (mobjSortColumns.Count == 0) {
                return 0;
            }

            return Compare (0, x, y);
        }

        public int Compare (object x, object y)
        {
            if (mobjSortColumns.Count == 0) {
                return 0;
            }

            return Compare (0, (IDocumentViewModel) x, (IDocumentViewModel) y);
        }

        int Compare (int sortColumnIndex, IDocumentViewModel objX, IDocumentViewModel objY)
        {
            var objSortColumn = default(DocumentsSortColumn);
            int intResult = 0;

            if (sortColumnIndex >= mobjSortColumns.Count) {
                return 0;
            }

            objSortColumn = (DocumentsSortColumn) mobjSortColumns [sortColumnIndex];

            var isLastCompare = sortColumnIndex == mobjSortColumns.Count - 1;
            if (objSortColumn.Direction == SortDirection.Ascending) {
                intResult = CompareValues (objSortColumn.ColumnName, objX, objY, isLastCompare);
            }
            else {
                intResult = CompareValues (objSortColumn.ColumnName, objY, objX, isLastCompare);
            }

            // Difference not found, sort by next sort column
            if (intResult == 0) {
                return Compare (sortColumnIndex + 1, objX, objY);
            }

            return intResult;
        }

        int CompareValues (string columnName, IDocumentViewModel x, IDocumentViewModel y, bool isLastCompare)
        {
            switch (columnName) {
                case DocumentDisplayColumn.COLUMN_SORTORDER:
                    return x.SortOrderIndex.CompareTo (y.SortOrderIndex);

                case DocumentDisplayColumn.COLUMN_CATEGORY:
                    return x.Category.CompareTo (y.Category);

                case DocumentDisplayColumn.COLUMN_CREATEDBY:
                    return x.CreatedByUser.CompareTo (y.CreatedByUser);

                case DocumentDisplayColumn.COLUMN_MODIFIEDBY:
                    return x.ModifiedByUser.CompareTo (y.ModifiedByUser);

                case DocumentDisplayColumn.COLUMN_CREATEDDATE:
                    if (isLastCompare) {
                        return x.CreatedDate.CompareTo (y.CreatedDate);
                    }
                    return x.CreatedDate.Date.CompareTo (y.CreatedDate.Date);

                case DocumentDisplayColumn.COLUMN_PUBLISHEDONDATE:
                    if (isLastCompare) {
                        return x.PublishedOnDate.CompareTo (y.PublishedOnDate);
                    }
                    return x.PublishedOnDate.Date.CompareTo (y.PublishedOnDate.Date);

                case DocumentDisplayColumn.COLUMN_MODIFIEDDATE:
                    if (isLastCompare) {
                        return x.ModifiedDate.CompareTo (y.ModifiedDate);
                    }
                    return x.ModifiedDate.Date.CompareTo (y.ModifiedDate.Date);

                case DocumentDisplayColumn.COLUMN_DESCRIPTION:
                    return x.Description.CompareTo (y.Description);

                case DocumentDisplayColumn.COLUMN_OWNEDBY:
                    return x.OwnedByUser.CompareTo (y.OwnedByUser);

                case DocumentDisplayColumn.COLUMN_SIZE:
                    return x.Size.CompareTo (y.Size);

                case DocumentDisplayColumn.COLUMN_TITLE:
                    return x.Title.CompareTo (y.Title);

                case DocumentDisplayColumn.COLUMN_CLICKS:
                    return x.Clicks.CompareTo (y.Clicks);

                case DocumentDisplayColumn.COLUMN_ISFEATURED:
                    return x.IsFeatured.CompareTo (y.IsFeatured);
            }

            return 0;
        }
    }
}
