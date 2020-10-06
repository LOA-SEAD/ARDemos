<%@ page import="br.ufscar.sead.loa.scalear.remar.Model3D" %>
<!DOCTYPE html>
<html>
<head>
    <!--Import Google Icon Font-->
    <link href="http://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!--Import materialize.css-->
    <link type="text/css" rel="stylesheet" href="/scalear/css/materialize.css" media="screen,projection"/>
    <link rel="stylesheet" type="text/css" href="/scalear/css/model3D.css">

    <!--Let browser know website is optimized for mobile-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="layout" content="main">
    <meta charset="utf-8">
    <g:javascript src="editableTable.js"/>
    <g:javascript src="scriptTable.js"/>
    <g:javascript src="validate.js"/>
    <g:javascript src="resource.js"/>

    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>

    <meta property="user-name" content="${userName}"/>
    <meta property="user-id" content="${userId}"/>

    <g:set var="entityName" value="${message(code: 'model3d.label', default: 'Model3D')}"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <g:javascript src="iframeResizer.contentWindow.min.js"/>

</head>

<body>
<div class="cluster-header">
    <p class="text-teal text-darken-3 left-align margin-bottom" style="font-size: 28px;">
        ScaleAR - Tabela de Modelos 3D
    </p>
</div>


<table class="highlight" id="table" style="margin-top: -30px;">
    <thead>
    <tr>
        <th>Selecionar</th>
        <th>Nome</th>
        <th>Link</th>
        <th>Ação</th>
    </tr>
    </thead>

    <tbody>
    <g:each in="${model3DList}" status="i" var="model3DInstance">
        <tr id="tr${model3DInstance.id}" class="selectable_tr ${(i % 2) == 0 ? 'even' : 'odd'} "
            style="cursor: pointer;"
            data-id="${model3DInstance.id}"
            data-owner-id="${model3DInstance.ownerId}"
            data-checked="false">
            <g:if test="${model3DInstance.author == userName}">

                <td class="_not_editable"><input class="filled-in" type="checkbox"> <label></label></td>

                <td name="resource_label">${fieldValue(bean: model3DInstance, field: "name")}</td>

                <td>${fieldValue(bean: model3DInstance, field: "source")}</td>

                <td><i onclick="_edit($(this.closest('tr')))" style="color: #7d8fff; margin-right:10px;"
                       class="fa fa-pencil"></i></td>

            </g:if>
            <g:else>
                <td class="_not_editable"><input class="filled-in" type="checkbox"> <label></label></td>

                <td name="resource_label"
                    data-resourceId="${model3DInstance.id}">${fieldValue(bean: model3DInstance, field: "name")}</td>

                <td>${fieldValue(bean: model3DInstance, field: "source")}</td>

                <td><i style="color: gray; margin-right:10px;" class="fa fa-pencil"></i>
                </td>
            </g:else>
        </tr>
    </g:each>
    </tbody>
</table>

<input type="hidden" id="editResourceLabel" value=""> <label for="editResourceLabel"></label>

<div class="row">
    <div class="col s2">
        <button class="btn waves-effect waves-light my-orange" type="submit" name="save" id="save">Enviar
        </button>
    </div>

    <div class="col s1 offset-s6">
        <a data-target="createModal" name="create"
           - class="btn-floating btn-large waves-effect waves-light modal-trigger my-orange tooltipped"
           data-tooltip="Criar questão"><i
                - class="material-icons">add</i></a>
    </div>

    <div class="col s1 m1 l1">
        <a onclick="_delete()" class=" btn-floating btn-large waves-effect waves-light my-orange tooltipped"
           data-tooltip="Excluir questão"><i class="material-icons">delete</i></a>
    </div>
</div>



<!-- Modal Structure -->
<div id="createModal" class="modal remar-modal">
    <g:form url="[resource: resourceInstance, action: 'newModel3D']">
        <div class="modal-content">
            <h4>Criar Modelo 3D<i class="material-icons tooltipped" data-position="right" data-delay="30"
                                data-tooltip="Criação de modelos 3D (nome, link)">info</i>
            </h4>

            <div class="row">
                <g:render template="form"/>
            </div>
        </div>

        <div class="modal-footer">
            <a href="#!" class="save modal-action modal-close btn waves-effect waves-light remar-orange" action="create"
               onclick="$(this).closest('form').submit()" name="create">Criar</a>
            <a href="#!" class="save modal-action modal-close btn waves-effect waves-light remar-orange">Cancelar</a>
        </div>
    </g:form>
</div>



<!-- Modal -->
<div id="editModal" class="modal remar-modal">
    <g:form url="[resource: resourceInstance, action: 'update']" method="PUT">
        <div class="modal-content">
            <h4>Editar Recurso</h4>

            <input id="editVersion" name="version" required="" value="" type="hidden">
            <input type="hidden" id="modelID" name="modelID">


            <div class="input-field col s12">
                <input id="editName" name="name" required="" value="" type="text" class="validate remar-input"
                       maxlength="150">
                <label id="nameLabel" for="editName">Nome</label>
            </div>

            <div class="input-field col s12">
                <input id="editSource" name="source" required="" value="" type="text" class="validate remar-input"
                       maxlength="500">
                <label id="sourceLabel" for="editSource">Link</label>
            </div>

            <div class="input-field col s12" style="display: none;">
                <input id="editAuthor" name="author" required="" readonly="readonly" value="" type="text"
                       class="validate remar-input">
                <label id="authorLabel" for="editAuthor">Autor</label>
            </div>

        </div>

        <div class="modal-footer">
            <a href="#!" class="save modal-action modal-close btn waves-effect waves-light remar-orange" action="update"
               onclick="$(this).closest('form').submit()" name="create">Atualizar</a>
            <a href="#!" class="modal-action modal-close btn waves-effect waves-light remar-orange">Cancelar</a>
        </div>
    </g:form>
</div>

<!-- Modal Structure -->
<div id="infoModal" class="modal">
    <div class="modal-content">
        <div id="totalResource">

        </div>
    </div>

    <div class="modal-footer">
        <button class="btn waves-effect waves-light modal-close my-orange">Entendi</button>
    </div>
</div>

<script type="text/javascript" src="/scalear/js/materialize.min.js"></script>
<script type="text/javascript">

    function changeEditResource(variable) {
        var editResource = document.getElementById("editResourceLabel");
        editResource.value = variable;

        console.log(editResource.value);
        //console.log(variable);
    }

</script>

</body>
</html>
