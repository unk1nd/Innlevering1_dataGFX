Innlevering1_dataGFX
====================

Opprett et nytt XNA-prosjekt for hver deloppgave.

Bruk WireFrame (device.RenderState.FillMode = FillMode.WireFrame) når du tegner disse figurene slik at du ser trekantene 
som tegnes.

a) Lag en kube vha. TriangleList. La hver side i kuben bestå av to trekanter - dvs. at det må anngis seks vertekser per
side i kuben. Hver side skal ha ulike farger. Lokaliser kuben rundt origo med sidelengder lik 1. 
Plasser kameraet i (3.5, 2.0, 5.0) slik at vi ser kuben skråstilt.

b) Lag en ny kube vha. TriangleStrip slik at man bruker færrest mulig vertekser. I del a) måtte man anngi 
36 vertekser - her skal det  holde å anngi 10 + 4 + 4 = 18. Du kan løse dette ved å tegne kuben i tre ulike 
deler sidekantene pluss topp og bunn. Dette kan f.eks. gjøres ved å kalle på DrawUserPrimitive(...) tre ganger. 
Du tegner en strip for alle sidekantene samt en for topp og en for bunn av kuben. 
Opprett tre vertekstabeller som inneholder vertekser for disse tre delene av kuben.
