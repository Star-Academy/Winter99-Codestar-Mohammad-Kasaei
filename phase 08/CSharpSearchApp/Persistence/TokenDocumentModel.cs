﻿namespace Persistence
{
    class TokenDocumentModel
    {
        public int TokenModelId { get; set; }
        public TokenModel TokenModel { get; set; }
        public int DocumentModelId { get; set; }
        public DocumentModel DocumentModel { get; set; }
    }
}
