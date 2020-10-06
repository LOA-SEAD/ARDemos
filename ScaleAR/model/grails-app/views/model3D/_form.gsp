<%@ page import="br.ufscar.sead.loa.scalear.remar.Model3D" %>
<link rel="stylesheet" type="text/css" href="/scalear/css/model3D.css">


<div class="input-field col s12">
    <input id="name" name="name" required="" value="${model3DInstance?.name}" type="text"
           class="validate remar-input" maxlength="150">
    <label for="name">Nome</label>
</div>

<div class="input-field col s12">
    <input id="source" name="source" required="" value="${model3DInstance?.source}" type="text"
           class="validate remar-input" maxlength="500">
    <label for="source">Link</label>
</div>

<div class="input-field col s12" style="display: none;">
    <input id="author" name="author" required="" readonly="readonly" value="${model3DInstance?.author}" type="text"
           class="validate remar-input">
    <label for="author">Autor</label>
</div>

